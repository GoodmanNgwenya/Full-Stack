import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    //get all user info
    getAllUsers():Observable<User[]> {
        return this.http.get<User[]>(`${environment.apiUrl}/users`)
            .pipe(
                map((data: User[]) => {
                    return data;
                }), catchError(error => {
                    return throwError(error);
                })
            )
    }

    //get user by Id
    getUserById(id: number):Observable<User> {
        return this.http.get(`${environment.apiUrl}/users/${id}`)
        .pipe(
            map((data: User) => {
                return data;
            }), catchError(error => {
                return throwError(error);
            })
        )
    }

    //create new user
    register(user: User):Observable<User> {
        return this.http.post(`${environment.apiUrl}/users/register`, user)
        .pipe(
            map((data: User) => {
              return data;
            }), catchError(error => {
              return throwError(error);
            })
          );
    }

    //update user details
    updateUser(id: number, user: User): Observable<User> {   
        return this.http.put(`${environment.apiUrl}/users/${id}`,user)
        .pipe(
            map((data: User) => {
              return data;
            }), catchError(error => {
              return throwError(error);
            })
          );
    }

}