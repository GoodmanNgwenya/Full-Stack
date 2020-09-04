import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Advert } from '@app/_models/advert';
import { environment } from '@environments/environment';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts`)
  }

  getById(id: number) {
    return this.http.get<Advert>(`${environment.apiUrl}/adverts/${id}`);
  }

  getAdvertsById(userId: number): Observable<Advert[]> {
    if (userId === 0) {
      of(this.initializeAdvert());
    }
    return this.http.post<Advert[]>(`${environment.apiUrl}/adverts`, { userId })
      .pipe(map(advert => {
        return advert;
      }));
  }

  create(advert: Advert) {
    return this.http.post(`${environment.apiUrl}/adverts/create`, advert);
  }

  update(id: number, advert:Advert) {
    return this.http.put(`${environment.apiUrl}/adverts/${id}`, advert);
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiUrl}/adverts/${id}`);
  }

  private initializeAdvert(): Advert {
    // Return an initialized object
    return {
      id: 0,
      advertHeadlineText: null,
      releaseDate: null,
      province: null,
      city: null,
      price: null,
      advertDetails: null,
      advertStatus: null,
      userId: 0,
      imageUrl: null
    };
  }

}
