import {Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";

import CryptoES from "crypto-es";
import {AccountService} from "../../services/account.service";
import {ILoginInput} from "../interfaces/login-input";
import {Router} from "@angular/router";
import {ValidationPatterns} from "../../../features/validation/validation-patterns";

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"]
})
export class LoginComponent {
  form: FormGroup = new FormGroup({
    email: new FormControl('admin@project.com', { validators: [Validators.required, Validators.pattern(ValidationPatterns.email)] }),
    password: new FormControl('Qwerty-1', { validators: [Validators.required, Validators.minLength(6)] }),
  });

  constructor(
    private router: Router,
    private accountService: AccountService
  ) {
  }

  ngOnInit() {

  }

  onLogin() {
    const crpPassword = CryptoES.SHA512(this.form.value["password"]).toString();

    const userObj: ILoginInput = {
      email: this.form.value["email"],
      password: crpPassword
    };

    this.accountService.signIn(userObj).subscribe(r => {
      this.router.navigate(["/"]);
    });
  }

}
