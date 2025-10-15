import { Component, input, output, inject } from '@angular/core';
import { RegisterCreds } from '../../../types/registerCreds';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../app/core/services/account-service';
@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private accountService = inject(AccountService);
  protected creds = {} as RegisterCreds;
  cancelRegister = output<boolean>();

  register(): void {
    this.accountService.register(this.creds).subscribe({
      next: response => {
        this.cancel();
      },
      error: error => console.error(error)
    });
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }
}
