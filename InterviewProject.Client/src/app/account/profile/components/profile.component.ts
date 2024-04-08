import {Component} from "@angular/core";

@Component({
  selector: "profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"]
})
export class ProfileComponent {
  user: any = {
    name: "Test",
    email: "test@test.com",
    role: "Admin"
  };

  constructor() {
  }


}
