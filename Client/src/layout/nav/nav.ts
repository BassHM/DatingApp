import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../app/core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router"
@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected credentials: any = {};
  protected accountService = inject(AccountService)
  protected loggedIn = signal(false);
  private router = inject(Router);

  login(): void {
    console.log(this.credentials);
    this.accountService.login(this.credentials).subscribe({
      next: response => {
        this.loggedIn.set(true);
        this.credentials = {};
        this.router.navigateByUrl('/members');
      },
      error: error => {
        alert(error.message);
      }
    });
  }

  logout(): void { 
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
