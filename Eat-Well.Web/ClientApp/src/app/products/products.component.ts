import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";
declare var $: any;

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.css"],
})
export class ProductsComponent implements OnInit {
  Products: any = {
    data: [],
    totalRecord: 0,
    page: 0,
    size: 10,
    totalPage: 0,
  };

  Product: any = {
    productId: 1,
    productName: "bao",
    categoryId: 1,
    photo: "dasda",
    description: "dsa",
  };

  isEdit: boolean = true;

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.GetAllProductWithPagination(1);
  }

  GetAllProductWithPagination(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page", cPage).set("size", "10");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http
      .get("https://localhost:44317/api/Products/pagination", { params })
      .subscribe(
        (result) => {
          this.Products = result;
          this.Products = this.Products.data;
        },
        (error) => console.error(error)
      );
  }

  Next() {
    if (this.Products.page < this.Products.totalPage) {
      let nextPage = this.Products.page + 1;
      let params = new HttpParams().set("page", nextPage).set("size", "10");
      this.http
        .get("https://localhost:44317/api/Products/pagination", { params })
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
        .get("https://localhost:44317/api/Products/pagination", { params })
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

  openModal(isNew, index) {
    if (isNew) {
      this.isEdit = false;
      this.Product = {
        productId: 0,
        productName: "",
        categoryId: 1,
        photo: "",
        description: "",
      };
    } else {
      this.isEdit = true;
      this.Product = index;
    }
    $("#exampleModal").modal("show");
  }

  addProduct() {
    var x = this.Product;
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

  saveProduct() {
    var x = this.Product;
    this.http.put("https://localhost:44317/api/Products", x).subscribe(
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
        this.Products = result;
        this.GetAllProductWithPagination(1);
      },
      (error) => console.error(error)
    );
  }
}
