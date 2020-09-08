import { DatePipe } from '@angular/common';

export class Advert {
  id: number;
  advertHeadlineText: string;
  province: string;
  city: string;
  advertDetails: string;
  price: number;
  releaseDate: string;
  imageUrl: string;
  advertStatus: string;
  userId: number;
}
