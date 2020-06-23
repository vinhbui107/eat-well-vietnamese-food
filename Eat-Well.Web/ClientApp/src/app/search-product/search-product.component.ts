import { Component, OnInit, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient, HttpParams } from "@angular/common/http";
@Component({
  selector: "app-search-product",
  templateUrl: "./search-product.component.html",
  styleUrls: ["../home/home.component.css"],
})
export class SearchProductComponent implements OnInit {
  Category: any = {
    id: Number,
    name: String,
    slug: String,
    products: [],
  };
  searchProducts: any = {
    data: [],
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private _Activatedroute: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.getbykey();
  }

  getbykey() {
    let key = window.localStorage.getItem("key");
    let params = new HttpParams()
      .set("key", key)
      .set("page", "1")
      .set("size", "10");
    this.http
      .get("https://localhost:44317/api/Products/search", { params })
      .subscribe(
        (result) => {
          this.searchProducts = result;
          this.searchProducts = this.searchProducts.data;
        },
        (error) => console.error(error)
      );
    window.localStorage.removeItem("key");
  }

  //Get Id Product
  getIdProduct(pro_id) {
    this.http.get("https://localhost:44317/api/Products/" + pro_id).subscribe(
      (result) => {
        this.router.navigate(["/product/" + pro_id]);
      },
      (error) => console.error(error)
    );
  }
}
