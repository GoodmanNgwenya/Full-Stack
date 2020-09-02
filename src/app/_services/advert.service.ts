import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Advert } from '@app/_models/advert';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Advert[]>(`${environment.apiUrl}/adverts`)
  }

  getById(id: number) {
    return this.http.get(`${environment.apiUrl}/adverts/${id}`);
  }

  create(advert: Advert) {
    return this.http.post(`${environment.apiUrl}/adverts/create`, advert);
  }

  update(id: number, params) {
    return this.http.put(`${environment.apiUrl}/${id}`, params);
  }

  delete(id: number) {
    return this.http.delete(`${environment.apiUrl}/${id}`);
  }

}
