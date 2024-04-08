import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"]
})
export class LoginComponent {
  form: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });


  constructor() {
  }

  onLogin() {

  }
}
