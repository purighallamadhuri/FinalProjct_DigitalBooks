import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksCompComponent } from './books-comp.component';

describe('BooksCompComponent', () => {
  let component: BooksCompComponent;
  let fixture: ComponentFixture<BooksCompComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BooksCompComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BooksCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
