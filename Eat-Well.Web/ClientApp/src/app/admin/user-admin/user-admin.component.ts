import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router , NavigationEnd} from "@angular/router";
declare var $:any;

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.css']
})
export class UserAdminComponent implements OnInit {
  Users: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };

  User:any = {
    id: Number,
    username: String,
    email: String,
    full_name: String,
    password: String,
    address: String,
    phone: String,
    is_admin: false,
    is_active: true,
  }
  isEdit: boolean = true;
  refeshdata: any;

  constructor(
    private http: HttpClient, private router:Router,
    @Inject("BASE_URL") baseUrl: string
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
    this.refeshdata = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.router.navigated = false;
      }
    });
  }
  ngOnDestroy() {
    if (this.refeshdata) {
      this.refeshdata.unsubscribe();
    }
  }
  ngOnInit() 
  {
    this.GetAllUserWithPagination(1);
    // this.getuseradminbyid(id);
  }

  GetAllUserWithPagination(cPage) {
    
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page",cPage).set("size", "10");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http.get("https://localhost:44317/api/Users", {params}).subscribe(
      result => {
        this.Users = result;
        this.Users = this.Users.data;
      },error => console.error(error)
    );
}



Next()
{
  if(this.Users.page < this.Users.total_page)
  {
    let nextPage = this.Users.page + 1;
    let params = new HttpParams().set("page",nextPage).set("size", "10");
    this.http.get("https://localhost:44317/api/Users", {params}).subscribe(
        result => {
          this.Users = result;
          this.Users = this.Users.data;
        },error => console.error(error)
      );
  }
    else
    {
      alert("Bạn đang ở trang cuối!");
    }
}

Previous()
{
  if(this.Users.page>1)
  {
    let PrePage = this.Users.page - 1;
    let params = new HttpParams().set("page",PrePage.toString()).set("size", "10");
    this.http.get("https://localhost:44317/api/Users", {params}).subscribe(
        result => {
          this.Users = result;
          this.Users = this.Users.data;
        },error => console.error(error)
      );
  }
  else
  {
    alert("Bạn đang ở trang đầu!");
  }
}

openModal(isNew,index)
  {
    if(isNew)
    {
      this.isEdit = false;
      this.User = {
        id:1,
    username: "",
    email:"",
    full_name: "",
    password: "",
    address: "",
    phone:""
      }
    }
    else
    {
      this.isEdit = true;
      this.User = index;
    }
    $('#exampleModal').modal("show");
  }


  

  addUser()
  {
    var x = {
      username: this.User.username,
      email: this.User.email,
      full_name: this.User.fullname,
      password: this.User.password,
      address: this.User.address,
      phone: this.User.phone,
      is_admin: false,
      is_active: true
    };
    console.log(x);
    this.http.post("https://localhost:44317/api/Users/Register", x).subscribe(
      result => {
        var res:any = result;
        if(res.success)
        {
          this.User = res.data;
          window.location.reload();
        }
      },error => console.error(error)
    );
  }

 

  updateUser(Id) {
    //console.log(id);
    var x = {
      id: Id,
      username: this.User.username,
      email: this.User.email,
      full_name: this.User.full_name,
      password: this.User.password,
      address: this.User.address,
      phone: this.User.phone,
      is_admin: this.User.is_admin,
      is_active: this.User.is_active,
    };
    this.http.put("https://localhost:44317/api/Users/" + Id, x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.User = res.data;
          window.location.reload();
          alert("Bạn đã cập nhật tài khoản thành công !");
        }
      },
      (error) => {
        console.error(error);
        alert("Bạn cập nhật tài khoản thất bại thành công !");

      }
    );
  }
  deleteUser(Id)
  {
    this.http.delete("https://localhost:44317/api/Users/" + Id).subscribe(
      result => {
        alert("Xóa tài khoản thành công!");
        this.Users = result;
        this.deleteUser(Id);
        window.location.reload();
      },error => console.error(error)
    );
  }


}