import { Component, OnInit, ViewChild } from '@angular/core';
import { ResultsService } from 'src/app/services/results.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-upload-results',
  templateUrl: './upload-results.component.html',
  styleUrls: ['./upload-results.component.css']
})
export class UploadResultsComponent implements OnInit {
  states;
  @ViewChild('fileInput') fileInput;
  private file: File;
  public uploading = false;
  public staged = false;

  constructor(private resultService: ResultsService, private sharedService: SharedService,) { }

  ngOnInit(): void {
    this.sharedService.states.subscribe(broj => this.states = broj);
  }

  public stageFile(): void {
    this.staged = true;
    this.file = this.fileInput.nativeElement.files[0];
    console.log(this.file)
  }

  public fileUpload(): void {
    this.uploading = true;
    if (this.file != null)
      this.resultService.uploadResultsFile(this.file).subscribe(
        res => {
          this.resultService.getAllResults().subscribe(
            data => {
              this.sharedService.changeStates(Object.assign(data));
              this.uploading = false;
              this.fileInput.nativeElement.value = '';
              this.file = null;
              this.staged = false;
            }
          );
        },
        err => {
          this.staged = false;
          this.uploading = false;
        });
  }

}

