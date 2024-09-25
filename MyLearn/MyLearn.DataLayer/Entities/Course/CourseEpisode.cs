﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }
        public int CourseId { get; set; }
        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string EpisodeTitle { get; set; }
        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }
        [Display(Name = "فایل")]
        public string EpisodeFileName { get; set; }
        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }

        #region Relations
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        #endregion
    }
}
