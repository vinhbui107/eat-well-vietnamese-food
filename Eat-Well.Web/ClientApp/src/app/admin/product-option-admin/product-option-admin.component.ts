import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router, NavigationEnd } from "@angular/router";
declare var $: any;
@Component({
  selector: "app-product-option-admin",
  templateUrl: "./product-option-admin.component.html",
  styleUrls: ["./product-option-admin.component.css"],
})
export class ProductOptionAdminComponent implements OnInit {
  ProductOptions: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };

  isEdit: boolean = true;

  ProductOption: any = {
    product_id: Number,
    product_name: String,
    id: Number,
    option_name: String,
    price: Number,
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.GetAllProductOptions(1);
  }

  GetAllProductOptions(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page", cPage).set("size", "10");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http
      .get("https://localhost:44317/api/ProductOptions", { params })
      .subscribe(
        (result) => {
          this.ProductOptions = result;
          this.ProductOptions = this.ProductOptions.data;
        },
        (error) => console.error(error)
      );
  }

  Next() {
    if (this.ProductOptions.page < this.ProductOptions.total_page) {
      let nextPage = this.ProductOptions.page + 1;
      let params = new HttpParams().set("page", nextPage).set("size", "10");
      this.http
        .get("https://localhost:44317/api/ProductOptions", { params })
        .subscribe(
          (result) => {
            this.ProductOptions = result;
            this.ProductOptions = this.ProductOptions.data;
          },
          (error) => console.error(error)
        );
    } else {
      alert("Bạn đang ở trang cuối!");
    }
  }

  Previous() {
    if (this.ProductOptions.page > 1) {
      let PrePage = this.ProductOptions.page - 1;
      let params = new HttpParams()
        .set("page", PrePage.toString())
        .set("size", "10");
      this.http
        .get("https://localhost:44317/api/ProductOptions", { params })
        .subscribe(
          (result) => {
            this.ProductOptions = result;
            this.ProductOptions = this.ProductOptions.data;
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
      this.ProductOption = {
        id: "",
        product_id: "",
        price: "",
      };
    } else {
      this.isEdit = true;
      this.ProductOption = index;
    }
    $("#exampleModal").modal("show");
  }

  addProductOptions() {
    var x = {
      id: Number(this.ProductOption.id),
      product_id: Number(this.ProductOption.product_id),
      price: Number(this.ProductOption.price),
    };
    console.log(x);
    this.http.post("https://localhost:44317/api/ProductOptions", x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.ProductOption = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  saveProductOptions(Id) {
    var x = {
      id: Id,
      product_id: Number(this.ProductOption.product_id),
      price: Number(this.ProductOption.price),
    };
    console.log(x);
    this.http
      .put("https://localhost:44317/api/ProductOptions/" + Id, x)
      .subscribe(
        (result) => {
          var res: any = result;
          if (res.success) {
            this.ProductOption = res.data;
            window.location.reload();
          }
        },
        (error) => console.error(error)
      );
  }
}
