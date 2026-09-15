import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Article, ArticlesPage } from '../models/article.models';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  constructor(private readonly http: HttpClient) {}

  getFeed(): Observable<Article[]> {
    return this.http.get<Article[]>(`${environment.apiUrl}/users/feed`);
  }

  getAll(pageNumber: number, pageSize: number): Observable<ArticlesPage> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    return this.http.get<ArticlesPage>(`${environment.apiUrl}/articles`, { params });
  }

  getById(articleId: string): Observable<Article> {
    return this.http.get<Article>(`${environment.apiUrl}/articles/${articleId}`);
  }

  likeArticle(articleId: string): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/articles/${articleId}/like`, {});
  }
}
