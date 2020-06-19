import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"],
})
export class RegisterComponent implements OnInit {
  account = {
    username: "",
    email: "",
    fullname: "",
    password: "",
    address: "",
    phone: "",
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  registerAccount(username, email, fullname, password, address, phone) {
    var x = {
      username: username,
      email: email,
      full_name: fullname,
      password: password,
      address: address,
      phone: phone,
      is_admin: false,
      is_active: true,
    };
    this.http.post("https://localhost:44317/api/Users/Register", x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          console.log(res.id);
          this.router.navigate(["/login"]);
          alert("Bạn đẫ đăng ký tài khoản thành công. <3");
        }
      },
      (error) => {
        console.error(error);
        alert("Đăng kí tài khoản không thành công!");
      }
    );
  }

  ngOnInit() {}
}
