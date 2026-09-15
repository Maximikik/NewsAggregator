import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/home/home').then((m) => m.Home),
  },
  {
    path: 'articles/:id',
    loadComponent: () =>
      import('./features/article-detail/article-detail').then((m) => m.ArticleDetail),
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
  },
  {
    path: 'feed',
    loadComponent: () => import('./features/feed/feed').then((m) => m.Feed),
    canActivate: [authGuard],
  },
  { path: '**', redirectTo: '' },
];
