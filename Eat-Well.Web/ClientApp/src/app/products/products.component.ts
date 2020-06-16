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
    total_record: 0,
    page: 0,
    size: 10,
    total_page: 0,
  };

  Product: any = {
    id: 1,
    name: "bao",
    category: [],
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
      .get("https://localhost:44317/api/Products", { params })
      .subscribe(
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

  openModal(isNew, index) {
    if (isNew) {
      this.isEdit = false;
      this.Product = {
        id: 0,
        name: "",
        category: [],
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

  saveProduct(id) {
    var x = this.Product;
    this.http.put("https://localhost:44317/api/Products/" + id, x).subscribe(
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

  deleteProduct(id) {
    this.http.delete("https://localhost:44317/api/Products/" + id).subscribe(
      (result) => {
        this.Products = result;
        this.GetAllProductWithPagination(1);
      },
      (error) => console.error(error)
    );
  }
}
