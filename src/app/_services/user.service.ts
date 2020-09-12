import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient, private messageService: MessageService) { }

    //get all user info
    getAllUsers() {
        return this.http.get<User[]>(`${environment.apiUrl}/users`)
            .pipe(
                tap(_ => this.log('fetched users')),
                catchError(this.handleError<User[]>('users', []))
            );
    }

    //get user by Id
    getUserById(id: number) {
        return this.http.get(`${environment.apiUrl}/users/${id}`)
            .pipe(
                tap(_ => this.log(`fetched user id=${id}`)),
                catchError(this.handleError<User>(`getUser id=${id}`))
            );
    }

    //create new user
    register(user: User) {
        return this.http.post(`${environment.apiUrl}/users/register`, user)
            .pipe(
                tap(data => this.log('addUser: ' + JSON.stringify(data))),
                catchError(this.handleError<User>('addUser'))
            );
    }

    //update user details
    updateUser(id: number, user: User): Observable<User> {
        return this.http.put(`${environment.apiUrl}/users/${id}`, user)
            .pipe(
                //return advert and update
                map(() => user,
                    catchError(this.handleError<User>('updateUser')))
            );
    }

    //handle errors
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log an advert service message with the MessageService */
    private log(message: string) {
        this.messageService.add(`UserService: ${message}`);
    }


}