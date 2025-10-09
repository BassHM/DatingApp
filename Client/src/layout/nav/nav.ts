import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected credentials: any = {};

  login(): void {
    console.log(this.credentials);
  }
}
