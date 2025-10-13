import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountServices {
  private http = inject(HttpClient);
  baseUrl = "http://localhost:5057/api/";

  login(creds: any) {
    return this.http.post(this.baseUrl + "account/login", creds);
  }
}
