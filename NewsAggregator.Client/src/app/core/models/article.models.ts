export interface Article {
  id: string;
  title: string;
  description: string;
  source: string;
}

export interface ArticlesPage {
  pageNumber: number;
  pageSize: number;
  articles: Article[];
}
