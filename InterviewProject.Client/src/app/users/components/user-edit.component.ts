import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: "user-edit",
  templateUrl: './user-edit.component.html',
  styleUrls: ["./user-edit.component.scss"]
})
export class UserEditComponent {

  form: FormGroup = new FormGroup({
    email: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    role: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });

  constructor() {
  }

  ngOnInit() {

  }

  save() {

  }
}
