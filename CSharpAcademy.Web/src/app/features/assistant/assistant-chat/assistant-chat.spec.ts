import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssistantChat } from './assistant-chat';

describe('AssistantChat', () => {
  let component: AssistantChat;
  let fixture: ComponentFixture<AssistantChat>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssistantChat],
    }).compileComponents();

    fixture = TestBed.createComponent(AssistantChat);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
