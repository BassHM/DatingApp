import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../app/core/services/account-service';
@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected credentials: any = {};
  protected accountService = inject(AccountService)
  protected loggedIn = signal(false);

  login(): void {
    console.log(this.credentials);
    this.accountService.login(this.credentials).subscribe({
      next: response => {
        console.log(response);
        this.loggedIn.set(true);
        this.credentials = {};
      },
      error: error => {
        alert(error.message);
      }
    });
  }

  logout(): void { 
    this.accountService.logout();
  }
}
