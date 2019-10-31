using System;

namespace AISIDemo.School.Domain
{
	public abstract class Grade
	{
		public abstract T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF);

		public class AGrade : Grade
		{
			internal AGrade() { }

			public override T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF) => onA.Invoke(this);
		}

		public class BGrade : Grade
		{
			internal BGrade() { }

			public override T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF) => onB.Invoke(this);
		}

		public class CGrade : Grade
		{
			internal CGrade() { }

			public override T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF) => onC.Invoke(this);
		}

		public class DGrade : Grade
		{
			internal DGrade() { }

			public override T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF) => onD.Invoke(this);
		}

		public class FGrade : Grade
		{
			internal FGrade() { }

			public override T Match<T>(Func<AGrade, T> onA, Func<BGrade, T> onB, Func<CGrade, T> onC, Func<DGrade, T> onD, Func<FGrade, T> onF) => onF.Invoke(this);
		}
	}
}