import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
declare var $: any;

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.css"],
})
export class ProfileComponent implements OnInit {
  User: any = {
    id: Number,
    username: String,
    email: String,
    full_name: String,
    password: String,
    address: String,
    phone: String,
    is_admin: false,
    is_active: true,
  };
  isEdit: boolean;

  constructor(
    private http: HttpClient,
    private router: Router,
    private _Activatedroute: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    let id = window.localStorage.getItem("customerId");
    this.getprofilebyid(id);
  }

  getprofilebyid(profile_id) {
    this.http.get("https://localhost:44317/api/Users/" + profile_id).subscribe(
      (result) => {
        this.User = result;
        this.User = this.User.data[0];
        console.log(this.User);
      },
      (error) => console.error(error)
    );
  }

  openModal(isNew, index) {
    if (isNew) {
      this.isEdit = false;
      this.User = {
        id: 0,
        username: "",
        full_name: "",
        email: "",
        address: "",
        phone: "",
        password: "",
        is_admin: false,
        is_active: true,
      };
    } else {
      this.isEdit = true;
      console.log("__setThisUser", index);
      this.User = index;
    }
    $("#exampleModal").modal("show");
  }

  saveProfile() {
    //console.log(id);
    var x = {
      id: this.User.id,
      username: this.User.username,
      email: this.User.email,
      full_name: this.User.full_name,
      password: this.User.password,
      address: this.User.address,
      phone: this.User.phone,
      is_admin: this.User.is_admin,
      is_active: this.User.is_active,
    };
    console.log("saveProfileData", x);
    this.http.put("https://localhost:44317/api/Users/" + x.id, x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.User = res.data;
          alert("Bạn đã cập nhật tài khoản thành công !");
          window.location.reload();
        }
      },
      (error) => {
        console.error(error);
        alert("Bạn đã cập nhật tài khoản thất bại !");
      }
    );
  }
}
