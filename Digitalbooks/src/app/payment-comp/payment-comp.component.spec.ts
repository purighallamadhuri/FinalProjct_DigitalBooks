import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentCompComponent } from './payment-comp.component';

describe('PaymentCompComponent', () => {
  let component: PaymentCompComponent;
  let fixture: ComponentFixture<PaymentCompComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentCompComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
