import {Component} from "@angular/core";
import {ConfirmDialogModel} from "../../features/confirm-dialog/classes/confirm-dialog-model";
import {ConfirmDialogComponent} from "../../features/confirm-dialog/components/confirm-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {map, Observable, tap} from "rxjs";
import {UserService} from "../services/user.service";
import {IUser} from "../interfaces/user";
import {PageEvent} from "@angular/material/paginator";
import {IFilterCommand} from "../../features/filter/interfaces/filter-command";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'users',
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.scss"]
})
export class UsersComponent {

  public users: IUser[] = [];
  public totalUsers: number = 0;

  public page = 1;
  public rowsPage = 5;

  searchValue: string = '';

  displayedColumns: string[] = ['id', 'name', 'email', 'roleName', 'actions'];

  constructor(
    public dialog: MatDialog,
    private userService: UserService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.getRemoteData();
  }

  // Get remote serve data using HTTP call
  getRemoteData() {
    const command = <IFilterCommand> {
      skip: (this.page - 1) * this.rowsPage,
      take: this.rowsPage,
      searchQuery: this.searchValue
    }

    this.userService.getPage(command).pipe(
      map(r => {
        this.totalUsers = r.total
        return r.items;
      })
    ).subscribe(u => {
      this.users = u;
    })
  }

  onPageChanged(event: PageEvent) {
    this.page = event.pageIndex + 1;

    this.getRemoteData();
  }

  executeFilter() {
    this.page = 1;

    this.getRemoteData();
  }

  onDelete(selectedUser: any) {
    const dialogData = new ConfirmDialogModel("Confirm Action", `Are you sure you want to remove this user?`);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.userService.delete(selectedUser.id).subscribe(r => {
          if(r) {
            this.executeFilter();
            this.toastrService.success("User removed successfully", "Remove")
          }
        })
      }
    });
  }
}
