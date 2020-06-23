import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-order-user",
  templateUrl: "./order-user.component.html",
  styleUrls: ["./order-user.component.css"],
})
export class OrderUserComponent implements OnInit {
  OrderUser: any = {
    data: [],
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private _Activatedroute: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.getOrderWithUserId(1);
  }
  getOrderWithUserId(user_id) {
    this.http
      .get("https://localhost:44317/api/Users/get-order-with-userId/" + user_id)
      .subscribe(
        (result) => {
          this.OrderUser = result;
          this.OrderUser = this.OrderUser.data;
          console.log(this.OrderUser);
        },
        (error) => console.error(error)
      );
  }
}
