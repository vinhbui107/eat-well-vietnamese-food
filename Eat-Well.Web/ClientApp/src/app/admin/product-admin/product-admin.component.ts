import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router, NavigationEnd } from "@angular/router";
declare var $: any;

@Component({
  selector: "app-product-admin",
  templateUrl: "./product-admin.component.html",
  styleUrls: ["./product-admin.component.css"],
})
export class ProductAdminComponent implements OnInit {
  Products: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };

  Product: any = {
    id: Number,
    name: String,
    category: [],
    photo: String,
    description: String,
    slug: String,
    is_active: Boolean,
    options: [],
  };
  isEdit: boolean = true;
  refeshdata: any;

  constructor(
    private http: HttpClient,
    private router: Router,
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

  ngOnInit() {
    this.GetAllProduct(1);
  }

  GetAllProduct(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page", cPage).set("size", "10");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http.get("https://localhost:44317/api/Products", { params }).subscribe(
      (result) => {
        this.Products = result;
        this.Products = this.Products.data;
      },
      (error) => console.error(error)
    );
  }

  Next() {
    if (this.Products.page < this.Products.total_page) {
      let nextPage = this.Products.page + 1;
      let params = new HttpParams().set("page", nextPage).set("size", "10");
      this.http
        .get("https://localhost:44317/api/Products", { params })
        .subscribe(
          (result) => {
            this.Products = result;
            this.Products = this.Products.data;
          },
          (error) => console.error(error)
        );
    } else {
      alert("Bạn đang ở trang cuối!");
    }
  }

  Previous() {
    if (this.Products.page > 1) {
      let PrePage = this.Products.page - 1;
      let params = new HttpParams()
        .set("page", PrePage.toString())
        .set("size", "10");
      this.http
        .get("https://localhost:44317/api/Products", { params })
        .subscribe(
          (result) => {
            this.Products = result;
            this.Products = this.Products.data;
          },
          (error) => console.error(error)
        );
    } else {
      alert("Bạn đang ở trang đầu!");
    }
  }

  //Modal
  openModal(isNew, index) {
    if (isNew) {
      this.isEdit = false;
      this.Product = {
        id: 1,
        name: "",
        category: [],
        photo: "",
        description: "",
        slug: "",
        is_active: Boolean,
        options: [],
      };
    } else {
      this.isEdit = true;
      this.Product = index;
    }
    $("#exampleModal").modal("show");
  }

  addProduct() {
    var x = {
      category_id: this.Product.category.id,
      name: this.Product.name,
      photo: this.Product.photo,
      description: this.Product.description,
      slug: "helloworld",
      is_active: true,
      options: [],
    };
    console.log(x);
    this.http.post("https://localhost:44317/api/Products", x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.Product = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  saveProduct(Id) {
    //   this.Product.options.forEach( (op) => {
    //     op.id = op.id.substring(0,10);
    // });
    for (let i = 0; i < this.Product.options.length; i++) {
      console.log(this.Product.options[i].price); //use i instead of 0
    }
    var x = {
      id: Id,
      category_id: this.Product.category.id,
      name: this.Product.name,
      photo: this.Product.photo,
      description: this.Product.description,
      slug: this.Product.slug,
      is_active: this.Product.is_active,
      options: [],
    };
    console.log(Id);
    console.log(x);
    this.http.put("https://localhost:44317/api/Products/" + Id, x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.Product = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  deleteProduct(Id) {
    this.http.delete("https://localhost:44317/api/Products/" + Id).subscribe(
      (result) => {
        alert("Xóa thành công!");
        this.Products = result;
        this.deleteProduct(Id);
        window.location.reload();
      },
      (error) => console.error(error)
    );
  }
}
