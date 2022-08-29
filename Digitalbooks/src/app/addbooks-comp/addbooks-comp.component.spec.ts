import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddbooksCompComponent } from './addbooks-comp.component';

describe('AddbooksCompComponent', () => {
  let component: AddbooksCompComponent;
  let fixture: ComponentFixture<AddbooksCompComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddbooksCompComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddbooksCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
