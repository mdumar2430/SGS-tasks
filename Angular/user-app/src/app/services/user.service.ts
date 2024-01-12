import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn : boolean = false;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>('https://localhost:44334/api/Users',{ responseType: 'json' })
  }

  userLogin(body:any):Observable<boolean>{
    return this.http.post<boolean>('https://localhost:44334/api/Login', body)
  }
}
