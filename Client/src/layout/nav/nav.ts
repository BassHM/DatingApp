import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountServices } from '../../app/core/services/account-services';
@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected credentials: any = {};
  protected accountService = inject(AccountServices)

  login(): void {
    console.log(this.credentials);
    this.accountService.login(this.credentials).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        alert(error.message);
      }
    });
  }
}
