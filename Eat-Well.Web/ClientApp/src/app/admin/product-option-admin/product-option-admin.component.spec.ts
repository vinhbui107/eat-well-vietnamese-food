import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductOptionAdminComponent } from './product-option-admin.component';

describe('ProductOptionAdminComponent', () => {
  let component: ProductOptionAdminComponent;
  let fixture: ComponentFixture<ProductOptionAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductOptionAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductOptionAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
