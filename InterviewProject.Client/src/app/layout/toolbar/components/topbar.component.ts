import {Component} from "@angular/core";
import {AccountService} from "../../../account/services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: "topbar",
  templateUrl: './topbar.component.html',
  styleUrls: ["./topbar.components.scss"]
})
export class TopbarComponent {
  constructor(
    private router: Router,
    public accountService: AccountService
  ) {
  }

  public get isCurrentUserAuthenticated() {
    return this.accountService.isLogged;
  }

  public get isCurrentUserAdmin() {
    return this.accountService.isAdmin;
  }

  logoutClicked() {
    this.accountService.logout().subscribe(r => {
      this.router.navigate(["/login"])
    })
  }
}
