    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.WebTesting;
    using SampleWebTestRules;
    using System;


namespace SampleWebTestRules
{

    public class WebTestDependentFilter : WebTestPlugin
    {

        string m_startsWith;

        public string FilterDependentRequestsThatStartWith
        {

            get { return m_startsWith; }

            set { m_startsWith = value; }

        }
        string m_endsWith;

        public string FilterDependentRequestsThatEndWith
        {

            get { return m_endsWith; }

            set { m_endsWith = value; }

        }
        string m_contains;

        public string FilterDependentRequestsThatContain
        {

            get { return m_contains; }

            set { m_contains = value; }

        }


        public override void PostRequest(object sender, PostRequestEventArgs e)
        {

            WebTestRequestCollection depsToRemove = new WebTestRequestCollection();



            // Note, you can't modify the collection inside a foreach, hence the second collection

            // requests to remove.

            foreach (WebTestRequest r in e.Request.DependentRequests)
            {

                if (!string.IsNullOrEmpty(FilterDependentRequestsThatStartWith) &&

                    r.Url.StartsWith(FilterDependentRequestsThatStartWith))
                {

                    depsToRemove.Add(r);

                }
                if (!string.IsNullOrEmpty(FilterDependentRequestsThatEndWith) &&
                    r.Url.EndsWith(FilterDependentRequestsThatEndWith))
                {

                    depsToRemove.Add(r);

                }

                if (!string.IsNullOrEmpty(FilterDependentRequestsThatContain) &&
                    r.Url.Contains(FilterDependentRequestsThatContain))
                {

                    depsToRemove.Add(r);

                }

            }

            foreach (WebTestRequest r in depsToRemove)
            {

                e.Request.DependentRequests.Remove(r);

            }

            WebTestDependentFilter DepFilter = new WebTestDependentFilter();
            DepFilter.FilterDependentRequestsThatContain = FilterDependentRequestsThatContain;
            DepFilter.FilterDependentRequestsThatEndWith = FilterDependentRequestsThatEndWith;
            DepFilter.FilterDependentRequestsThatStartWith = FilterDependentRequestsThatStartWith;
 
            foreach (WebTestRequest r in e.Request.DependentRequests)
            {
                r.PreRequest += new EventHandler<PreRequestEventArgs>(DepFilter.PreRequest);
                r.PostRequest += new EventHandler<PostRequestEventArgs>(DepFilter.PostRequest);

            }
        }

    }

}