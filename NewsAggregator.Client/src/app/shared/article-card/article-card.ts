import { Component, input, output, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Article } from '../../core/models/article.models';

@Component({
  selector: 'app-article-card',
  imports: [RouterLink],
  templateUrl: './article-card.html',
})
export class ArticleCard {
  readonly article = input.required<Article>();
  readonly isLiked = input(false);
  readonly showLikeButton = input(true);
  readonly liked = output<string>();

  readonly imageFailed = signal(false);

  onLike(): void {
    this.liked.emit(this.article().id);
  }

  onImageError(): void {
    this.imageFailed.set(true);
  }
}
