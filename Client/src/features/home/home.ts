import { Component, signal, Input } from '@angular/core';
import { Register } from "../account/register/register";
@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  protected registerMode = signal<boolean>(false);

  showRegister(value: boolean): void {
    this.registerMode.set(value);
  }
}
