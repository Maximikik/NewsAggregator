export interface Article {
  id: string;
  title: string;
  description: string;
  source: string;
  imageUrl: string | null;
}

export interface ArticlesPage {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  articles: Article[];
}
