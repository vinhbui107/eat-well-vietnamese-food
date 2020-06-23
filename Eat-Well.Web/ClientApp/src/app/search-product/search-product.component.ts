import { Component, OnInit, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
@Component({
  selector: "app-search-product",
  templateUrl: "./search-product.component.html",
  styleUrls: ["./search-product.component.css"],
})
export class SearchProductComponent implements OnInit {
  Category: any = {
    id: Number,
    name: String,
    slug: String,
    products: [],
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
    this.http
      .get("https://localhost:44317/api/Products/search" + key)
      .subscribe(
        (result) => {
          this.Category = result;
          this.Category = this.Category.data[0];
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
