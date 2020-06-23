import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-revenue-admin",
  templateUrl: "./revenue-admin.component.html",
  styleUrls: ["./revenue-admin.component.css"],
})
export class RevenueAdminComponent implements OnInit {
  Revenue: any = {
    data: [],
  };
  constructor(
    private http: HttpClient,
    private router: Router,
    private _Activatedroute: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.GetRevenue(6, 2020);
  }

  GetRevenue(Month, Year) {
    let params = new HttpParams().set("month", Month).set("year", Year);
    this.http
      .get("https://localhost:44317/api/Orders/get-revenue-with-month", {
        params,
      })
      .subscribe(
        (result) => {
          this.Revenue = result;
          this.Revenue = this.Revenue.data;
        },
        (error) => console.error(error)
      );
  }
}
