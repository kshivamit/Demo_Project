using CandidateDL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repository
{
    public interface ICandidateRepository
    {
        bool CreateCandidate(Candidate model);
        bool UpdateCandidate(Candidate model);
        bool DeleteCandidate(int Id);
        IEnumerable<Candidate> GetCandidates();
        Candidate GetCandidateById(int Id);
    }
}
