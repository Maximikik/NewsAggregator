import { Component, OnInit, signal } from '@angular/core';
import { ArticleService } from '../../core/services/article.service';
import { Article } from '../../core/models/article.models';
import { ArticleCard } from '../../shared/article-card/article-card';

@Component({
  selector: 'app-feed',
  imports: [ArticleCard],
  templateUrl: './feed.html',
})
export class Feed implements OnInit {
  readonly articles = signal<Article[]>([]);
  readonly likedIds = signal<Set<string>>(new Set());
  readonly isLoading = signal(true);
  readonly errorMessage = signal<string | null>(null);

  constructor(private readonly articleService: ArticleService) {}

  ngOnInit(): void {
    this.loadFeed();
  }

  loadFeed(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.articleService.getFeed().subscribe({
      next: (articles) => {
        this.articles.set(articles);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load your feed. Please try again.');
        this.isLoading.set(false);
      },
    });
  }

  onLike(articleId: string): void {
    this.articleService.likeArticle(articleId).subscribe({
      next: () => {
        this.likedIds.update((ids) => new Set(ids).add(articleId));
      },
    });
  }
}
