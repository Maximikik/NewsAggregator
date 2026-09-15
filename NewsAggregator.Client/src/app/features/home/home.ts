import { Component, OnInit, computed, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { ArticleService } from '../../core/services/article.service';
import { Article } from '../../core/models/article.models';
import { ArticleCard } from '../../shared/article-card/article-card';

const PAGE_SIZE = 9;

@Component({
  selector: 'app-home',
  imports: [RouterLink, ArticleCard],
  templateUrl: './home.html',
})
export class Home implements OnInit {
  readonly articles = signal<Article[]>([]);
  readonly likedIds = signal<Set<string>>(new Set());
  readonly isLoading = signal(true);
  readonly errorMessage = signal<string | null>(null);
  readonly pageNumber = signal(1);
  readonly totalCount = signal(0);

  readonly totalPages = computed(() => Math.max(1, Math.ceil(this.totalCount() / PAGE_SIZE)));
  readonly hasNextPage = computed(() => this.pageNumber() < this.totalPages());
  readonly hasPreviousPage = computed(() => this.pageNumber() > 1);

  constructor(
    protected readonly authService: AuthService,
    private readonly articleService: ArticleService,
  ) {}

  ngOnInit(): void {
    this.loadPage(1);
  }

  loadPage(page: number): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.articleService.getAll(page, PAGE_SIZE).subscribe({
      next: (result) => {
        this.articles.set(result.articles);
        this.pageNumber.set(page);
        this.totalCount.set(result.totalCount);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load articles. Please try again.');
        this.isLoading.set(false);
      },
    });
  }

  nextPage(): void {
    if (this.hasNextPage()) {
      this.loadPage(this.pageNumber() + 1);
    }
  }

  previousPage(): void {
    if (this.hasPreviousPage()) {
      this.loadPage(this.pageNumber() - 1);
    }
  }

  firstPage(): void {
    if (this.hasPreviousPage()) {
      this.loadPage(1);
    }
  }

  lastPage(): void {
    if (this.hasNextPage()) {
      this.loadPage(this.totalPages());
    }
  }

  onLike(articleId: string): void {
    this.articleService.likeArticle(articleId).subscribe({
      next: () => {
        this.likedIds.update((ids) => new Set(ids).add(articleId));
      },
    });
  }
}
