import { HttpClient } from '@angular/common/http';
import { Injectable, computed, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AuthResponse,
  LoginRequest,
  LogoutRequest,
  RefreshRequest,
  RegisterRequest,
} from '../models/auth.models';

const ACCESS_TOKEN_KEY = 'na_access_token';
const REFRESH_TOKEN_KEY = 'na_refresh_token';

interface JwtPayload {
  sub?: string;
  email?: string;
  exp?: number;
}

function decodeJwtPayload(token: string): JwtPayload | null {
  try {
    const payload = token.split('.')[1];
    const normalized = payload.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(atob(normalized)) as JwtPayload;
  } catch {
    return null;
  }
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly accessToken = signal<string | null>(localStorage.getItem(ACCESS_TOKEN_KEY));

  readonly isAuthenticated = computed(() => this.accessToken() !== null);

  readonly userEmail = computed(() => {
    const token = this.accessToken();
    if (!token) {
      return null;
    }
    return decodeJwtPayload(token)?.email ?? null;
  });

  constructor(private readonly http: HttpClient) {}

  getAccessToken(): string | null {
    return this.accessToken();
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(REFRESH_TOKEN_KEY);
  }

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${environment.apiUrl}/users/login`, request)
      .pipe(tap((response) => this.storeSession(response)));
  }

  register(request: RegisterRequest): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/users/register`, request);
  }

  refresh(): Observable<AuthResponse> {
    const refreshToken = this.getRefreshToken();
    const request: RefreshRequest = { refreshToken: refreshToken ?? '' };

    return this.http
      .post<AuthResponse>(`${environment.apiUrl}/users/refresh`, request)
      .pipe(tap((response) => this.storeSession(response)));
  }

  logout(): Observable<void> {
    const refreshToken = this.getRefreshToken();
    const request: LogoutRequest = { refreshToken: refreshToken ?? '' };

    return this.http
      .post<void>(`${environment.apiUrl}/users/logout`, request)
      .pipe(tap(() => this.clearSession()));
  }

  clearSession(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    this.accessToken.set(null);
  }

  private storeSession(response: AuthResponse): void {
    localStorage.setItem(ACCESS_TOKEN_KEY, response.accessToken);
    localStorage.setItem(REFRESH_TOKEN_KEY, response.refreshToken);
    this.accessToken.set(response.accessToken);
  }
}
