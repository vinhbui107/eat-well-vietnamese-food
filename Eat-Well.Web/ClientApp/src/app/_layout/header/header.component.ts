import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import { add, total, list } from "cart-localstorage";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.css"],
})
export class HeaderComponent implements OnInit {
  Categories: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };
  refeshdata: any;

  Customer: any = {
    id: Number,
    username: "",
  };

  Cart: any = {
    quantity: 0,
  };

  search: any = {
    key: "",
  };
  isLogin: boolean = false;

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string // refesh data
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
  // refesh data

  ngOnInit() {
    this.GetCategories(1);
    this.checkUserLogin();
    this.countCart();
  }

  GetCategories(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page", cPage).set("size", "10");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http
      .get("https://localhost:44317/api/Categories", { params })
      .subscribe(
        (result) => {
          this.Categories = result;
          this.Categories = this.Categories.data;
        },
        (error) => console.error(error)
      );
  }

  getbyid(id) {
    this.http.get("https://localhost:44317/api/Categories/" + id).subscribe(
      (result) => {
        this.router.navigate(["/category/" + id]);
      },
      (error) => console.error(error)
    );
  }

  checkUserLogin() {
    var cusomerId = window.localStorage.getItem("customerId");
    if (cusomerId != null) {
      this.isLogin = true;
      this.Customer.username = window.localStorage.getItem("username");
    } else {
      this.isLogin = false;
    }
  }

  logout() {
    window.localStorage.clear();
    this.router.navigate(["/login"]);
  }

  countCart() {
    let n = list().length;
    if (n == null) {
      this.Cart.quantity = 0;
    } else {
      this.Cart.quantity = n;
    }
  }

  searchProduct() {
    window.localStorage.setItem("key", this.search.key);
  }
}
