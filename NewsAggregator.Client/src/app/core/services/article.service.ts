import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Article } from '../models/article.models';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  constructor(private readonly http: HttpClient) {}

  getFeed(): Observable<Article[]> {
    return this.http.get<Article[]>(`${environment.apiUrl}/users/feed`);
  }

  likeArticle(articleId: string): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/articles/${articleId}/like`, {});
  }
}
