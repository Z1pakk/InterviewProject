import {Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import { ValidationPatterns } from "../../features/validation/validation-patterns";
import {IUser} from "../interfaces/user";
import {ActivatedRoute, Router} from "@angular/router";
import {UserService} from "../services/user.service";
import {getFormFieldError} from "../../utils/get-from-error";
import {ToastrService} from "ngx-toastr";
import CryptoES from "crypto-es";
import {deepCopy} from "../../utils/deepCopy";

@Component({
  selector: "user-edit",
  templateUrl: './user-edit.component.html',
  styleUrls: ["./user-edit.component.scss"]
})
export class UserEditComponent {
  protected readonly getFormFieldError = getFormFieldError;

  public user: IUser = <IUser>{};

  public isCreating = true;

  userForm: FormGroup = new FormGroup({
    email: new FormControl('', {validators: [Validators.required, Validators.pattern(ValidationPatterns.email)]}),
    firstName: new FormControl('', {validators: [Validators.required, Validators.maxLength(20)]}),
    lastName: new FormControl('', {validators: [Validators.required, Validators.maxLength(20)]}),
    roleName: new FormControl('', {validators: [Validators.required]}),
    password: new FormControl('', {validators: [Validators.minLength(6), Validators.maxLength(20)]}),
    confirmPassword: new FormControl('', {validators: [Validators.minLength(6), Validators.maxLength(20)]}),
  });

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    if (this.router.url == "/users/create") {
      this.isCreating = true;
      this.userForm.controls['password'].addValidators(Validators.required);
      this.userForm.controls['confirmPassword'].addValidators(Validators.required);
    } else {
      this.route.params.subscribe(async params => {
        const id = <string>params["id"];
        if (id && id != "0") {
          this.userService.get(id).pipe().subscribe(u => {
            this.updateForm(u);
          });
        }
      });
    }
  }

  private updateForm(user: IUser) {
    this.isCreating = false;

    this.user = user;
    this.userForm.patchValue({
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName,
      roleName: user.roleName
    });
  }

  save() {
    if (this.userForm.invalid) {
      this.toastrService.error("Form fields are not valid", "Update")
      return;
    }

    const user = deepCopy(this.userForm.value) as IUser;
    if (user.password) {
      user.password = CryptoES.SHA512(user.password).toString();
    }

    const action$ = this.user && this.user.id ? this.userService.update(this.user.id, user) : this.userService.create(user)
    action$.subscribe(r => {
      this.toastrService.success(`User ${this.isCreating ? 'created' : 'updated'} successfully`, "Update")
      this.updateForm(r);
      this.router.navigate(["/users"])
    })
  }
}
