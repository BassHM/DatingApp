import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User, LoginCreds } from '../../../types/user';
import { Observable, tap } from 'rxjs';
import { RegisterCreds } from '../../../types/registerCreds';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = "http://localhost:5057/api/";
  currentUser = signal<User | null>(null);

  login(creds: LoginCreds): Observable<User> {
    return this.http.post<User>(this.baseUrl + "account/login", creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }
  
  register(creds: RegisterCreds): Observable<User> {
    return this.http.post<User>(this.baseUrl + "account/register", creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem("user", JSON.stringify(user));
    this.currentUser.set(user);
  }

  logout() {
    localStorage.removeItem("user");
    this.currentUser.set(null);
  }
}
