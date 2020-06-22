import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";
declare var $: any;

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  account = {
    username: "",
    password: "",
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {}

  userLogin(userName, userPassword) {
    var x = {
      username: userName,
      password: userPassword,
    };
    this.http
      .post("https://localhost:44317/api/Users/Authenticate", x)
      .subscribe(
        (result) => {
          var res: any = result;
          if (res.id != null) {
            localStorage.setItem("customerId", res.id);
            localStorage.setItem("username", x.username);
            this.router.navigate(["/"]);
            alert("Đăng nhập thành công.");
          }
        },
        (error) => {
          console.error(error);
          alert("Đăng nhập thất bại.");
        }
      );
  }
}
