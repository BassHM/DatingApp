import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router"
import { ToastService } from '../../core/services/toast-service';
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
  private toast = inject(ToastService);

  login(): void {
    console.log(this.credentials);
    this.accountService.login(this.credentials).subscribe({
      next: response => {
        this.loggedIn.set(true);
        this.credentials = {};
        this.router.navigateByUrl('/members');
        this.toast.success("Logged in!");
      },
      error: error => {
        this.toast.error(error.message);
      }
    });
  }

  logout(): void { 
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
