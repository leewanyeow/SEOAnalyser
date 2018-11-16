import { Component, Inject, Directive } from '@angular/core';
import { Http } from '@angular/http';
@Component({
    selector: 'home',
    templateUrl: './home.component.html'

})
@Directive({
    selector: '[sortable-table]'
})
export class HomeComponent {
    public wordOccurs: WordOccurance[];
    public metaTags: MetaTag[];
    public externalLinks: ExternalLink[];
     
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
       
    }

    public SearchText() {
        var searchText;
        let enableStopWordCheck: boolean, numberOfWordAppearCheck: boolean, metaTagCheck: boolean, externalLinkCheck: boolean;
        if (typeof document !== 'undefined') {
            enableStopWordCheck = (<HTMLInputElement>document.getElementById("stopword")).checked;
            numberOfWordAppearCheck = (<HTMLInputElement>document.getElementById("eachword")).checked;
            metaTagCheck = (<HTMLInputElement>document.getElementById("metatag")).checked;
            externalLinkCheck = (<HTMLInputElement>document.getElementById("externallink")).checked;

            searchText = (<HTMLInputElement>document.getElementById("SearchTextInput")).value;

           
                this.http.get(this.baseUrl + 'api/WordOccurance/getWordOccurance?enablestopword=' + enableStopWordCheck + '&text=' + searchText).subscribe(result => {
            this.wordOccurs = result.json() as WordOccurance[];
            }, error => console.error(error));

            if (numberOfWordAppearCheck) {
                this.http.get(this.baseUrl + 'api/WordOccurance/getWordOccurance?enablestopword=' + enableStopWordCheck + '&text=' + searchText).subscribe(result => {
                    this.wordOccurs = result.json() as WordOccurance[];
                }, error => console.error(error));

            }
            if (metaTagCheck) {
                this.http.get(this.baseUrl + 'api/MetaTag/GetMetaTagList?enablestopword=' + enableStopWordCheck + '&text=' + searchText).subscribe(result => {
                    this.metaTags = result.json() as MetaTag[];
                }, error => console.error(error));

            }
            if (externalLinkCheck) {
                this.http.get(this.baseUrl + 'api/ExternalLink/GetExternalLinkList?enablestopword=' + enableStopWordCheck + '&text=' + searchText).subscribe(result => {
                    this.externalLinks = result.json() as ExternalLink[];
                }, error => console.error(error));
                
            }
            
        }
    }

  
    ShowWO: boolean = true;  
    public showWordOccurance() {
        this.ShowWO = true;
        this.ShowMT = false;
        this.ShowEL = false;
    }

    ShowMT: boolean = false;  
    public showMetaTag() {
        this.ShowWO = false;
        this.ShowMT = true;
        this.ShowEL = false;
    }

    ShowEL: boolean = false;  
    public showExternalLink() {
        this.ShowWO = false;
        this.ShowMT = false;
        this.ShowEL = true;
    }
}
interface MetaTag {
    name: string;
    content: string;
}
interface WordOccurance {
    word: string;
    count: number;
}
interface ExternalLink {
    name: string;
    link: number;
}