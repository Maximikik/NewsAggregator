import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { ArticleService } from '../../core/services/article.service';
import { Article } from '../../core/models/article.models';

@Component({
  selector: 'app-article-detail',
  imports: [RouterLink],
  templateUrl: './article-detail.html',
})
export class ArticleDetail implements OnInit {
  readonly article = signal<Article | null>(null);
  readonly isLoading = signal(true);
  readonly errorMessage = signal<string | null>(null);
  readonly isLiked = signal(false);
  readonly imageFailed = signal(false);

  constructor(
    private readonly route: ActivatedRoute,
    private readonly articleService: ArticleService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        switchMap((params) => {
          this.isLoading.set(true);
          this.errorMessage.set(null);
          this.isLiked.set(false);
          this.imageFailed.set(false);
          return this.articleService.getById(params.get('id')!);
        }),
      )
      .subscribe({
        next: (article) => {
          this.article.set(article);
          this.isLoading.set(false);
        },
        error: () => {
          this.errorMessage.set('This article could not be found.');
          this.isLoading.set(false);
        },
      });
  }

  onLike(): void {
    const current = this.article();
    if (!current) {
      return;
    }

    this.articleService.likeArticle(current.id).subscribe({
      next: () => this.isLiked.set(true),
    });
  }

  onImageError(): void {
    this.imageFailed.set(true);
  }
}
