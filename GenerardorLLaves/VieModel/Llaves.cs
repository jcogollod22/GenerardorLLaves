using System.ComponentModel.DataAnnotations;

namespace GenerardorLLaves.VieModel
{
    public class Llaves
    {
        
       
        [Display(Name = "Component 1")]
        public string numberOnehexadecimal { get; set; }
        [Display(Name = "Component 2")]
        public string numberTwohexadecimal { get; set; }
        [Display(Name = "Combined Key")]
        public string numberBinario { get; set; }


    }
}
