import {Component} from "@angular/core";
import {AccountService} from "../../services/account.service";
import {IUser} from "../../../users/interfaces/user";

@Component({
  selector: "profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"]
})
export class ProfileComponent {
  user: IUser = <IUser>{};

  constructor(
    private accountService: AccountService
  ) {
  }

  ngOnInit() {
    this.getProfile();
  }

  private getProfile() {
    this.accountService.me().subscribe(profile => {
      this.user = profile;
    })
  }
}
