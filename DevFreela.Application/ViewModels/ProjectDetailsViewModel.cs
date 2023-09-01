﻿namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(int id, string title, string description
            , decimal totalCost, DateTime? startedTime, DateTime? finishedAt
            , string clientFullName, string freelancerFullName )
        {
            Id = id;
            Title = title;
            Description = description;
            TotalCost = totalCost;
            StartedTime = startedTime;
            FinishedAt = finishedAt;
            ClientFullName = clientFullName;
            FreeLancerFullName = freelancerFullName;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedTime { get;  private set; }
        public DateTime? FinishedAt { get; private  set; }

        public string  ClientFullName { get; set; }
        public string FreeLancerFullName { get; set; }

    }
}