import { Component, input, output } from '@angular/core';
import { Article } from '../../../core/models/article.models';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.html',
})
export class ArticleCard {
  readonly article = input.required<Article>();
  readonly isLiked = input(false);
  readonly liked = output<string>();

  onLike(): void {
    this.liked.emit(this.article().id);
  }
}
